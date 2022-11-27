namespace Universe.Application.Dtos
{
    public class ComboboxItemDto
    {
        public virtual string? Value { get; set; }
        public virtual string? DisplayText { get; set; }
        public virtual bool IsSelected { get; set; }
        public ComboboxItemDto()
        {

        }
        public ComboboxItemDto(string value, string displayText)
        {
            Value = value;
            DisplayText = displayText;
        }
    }
}
