namespace TaskableApp.ViewModels
{
    public class ParameterItemViewModel
    {
        public string ParameterValue { get; set; }

        public ParameterItemViewModel(string parameterValue)
        {
            this.ParameterValue = parameterValue;
        }

        public override string ToString()
        {
            return this.ParameterValue;
        }
    }
}
