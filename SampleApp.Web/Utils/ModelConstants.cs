namespace SampleApp.Web.Utils
{
    public static class ModelConstants
    {
        public const int PasswordMinLength = 3;
        public const int PasswordMaxLength = 100;

        public const string MinLengthErrorMessage =
            "The {0} must be at least {1} characters long.";

        public const string MaxLengthErrorMessage =
            "The {0} must be at max {1} characters long.";
    }
} 