namespace toomuchtodo_backend;

public class Key
{
    public static string Secret = Environment.GetEnvironmentVariable("SECRET_KEY");
}