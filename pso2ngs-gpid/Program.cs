const string ProgramName = "pso2ngs-gpid";
const string Version = "0.1.0";
const string EnvAppCycle = "PSO2NGSGPID_CYCLE";
const string EnvAppStart = "PSO2NGSGPID_START";
const string DefaultCycle = "13,0941852763674535496307218129,1,01";
const string DefaultStart = "2023/06/07T04:00:00+0900";
bool verbose = false;
string[] unnamed = Array.Empty<string>();

ParseArgs(args);

string cycle_table = GetCycleTable(EnvAppCycle, DefaultCycle);
Verbose($"cycle table: {cycle_table}");
Verbose($"length: {cycle_table.Length}");

DateTime start = GetStartTime(EnvAppStart, DefaultStart);
Verbose($"start date: {start.ToString("yyyy-MM-ddTHH:mm:sszzz")}");

DateTime target_time = GetTargetTime(unnamed);
Verbose($"current date: {target_time.ToString("yyyy-MM-ddTHH:mm:sszzz")}");

int days_passed = (target_time - start).Days;
Verbose($"days passed: {days_passed}");
int index = days_passed % cycle_table.Length;
Verbose($"cycle index (starts with 0): {index}");
string gpid = $"gpid{cycle_table[index]}";
Console.WriteLine(gpid);

void ParseArgs(string[] args)
    {
    for (int i = 0; i < args.Length; i++)
        {
        string arg = args[i];
        switch (arg)
            {
            case "--":
                unnamed = args[(i + 1)..];
                i = args.Length; // break outer loop
                continue;
            case var _ when !arg.StartsWith('-'):
                unnamed = args[i..];
                i = args.Length; // break outer loop
                continue;
            case "--version":
            case "-v":
                PrintVersion();
                Environment.Exit(0);
                break;
            case "--help":
            case "-h":
                PrintUsage();
                Environment.Exit(0);
                break;
            case "--verbose":
            case "-V":
                verbose = true;
                break;
            default:
                ErrorExit($"Unknown option: {arg}");
                break;
            }
        }

    }

void PrintVersion()
    {
    Console.WriteLine($"{ProgramName} {Version}");
    }

void PrintUsage()
    {
    Console.WriteLine($"Usage: {ProgramName} [options] [target]");
    Console.WriteLine("Options:");
    Console.WriteLine("  -h, --help          Show this help message");
    Console.WriteLine("  -v, --version       Show program version");
    Console.WriteLine("  -V, --verbose       Enable verbose output");
    Console.WriteLine("Environment Variables:");
    Console.WriteLine("  PSO2NGSGPID_CYCLE   GPID cycle setting in format: count1,cycle1,count2,cycle2,...");
    Console.WriteLine("                      (default: 13,0941852763674535496307218129,1,01)");
    Console.WriteLine("  PSO2NGSGPID_START   Start date in ISO8601 format");
    Console.WriteLine("                      (default: 2023/06/07T04:00:00+0900)");
    Console.WriteLine("Target:");
    Console.WriteLine("  Optional target time in ISO8601 format. Defaults to current time if not provided.");
    }

void Verbose(string message)
    {
    if (verbose)
        {
        Console.Error.WriteLine("VERBOSE: " + message);
        }
    }

void ErrorExit(string message)
    {
    Console.Error.WriteLine("ERROR: " + message);
    Environment.Exit(1);
    }

string GetCycleTable(string varName, string defaultValue)
    {
    string env_CYCLE = Environment.GetEnvironmentVariable(varName) ?? defaultValue;
    string[] cycle_setting = env_CYCLE.Split(',', StringSplitOptions.RemoveEmptyEntries);
    if (cycle_setting.Length % 2 == 1 || cycle_setting.Length < 2)
        {
        ErrorExit($"Invalid {varName} setting.");
        }

    string cycle_table = "";
    for (int i = 0; i < cycle_setting.Length; i += 2)
        {
        if (!int.TryParse(cycle_setting[i], out int count))
            {
            ErrorExit($"Invalid {varName} format: {cycle_setting[i]}");
            }
        string cycle_fragment = cycle_setting[i + 1];
        cycle_table += string.Concat(Enumerable.Repeat(cycle_fragment, count));
        }
    return cycle_table;
    }

DateTime GetStartTime(string varName, string defaultValue)
    {
    string env_START = Environment.GetEnvironmentVariable(varName) ?? defaultValue;
    if (!DateTime.TryParse(env_START, out DateTime start))
        {
        ErrorExit($"Invalid {varName} date format.");
        }
    return start;
    }

DateTime GetTargetTime(string[] unnamed)
    {
    if (unnamed.Length == 0)
        {
        return DateTime.Now;
        }
    if (!DateTime.TryParse(unnamed[0], out DateTime target_time))
        {
        ErrorExit("Invalid date format: " + unnamed[0]);
        }
    return target_time;
    }
