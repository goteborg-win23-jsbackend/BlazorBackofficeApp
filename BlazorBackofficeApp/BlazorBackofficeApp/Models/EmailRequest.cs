namespace BlazorBackofficeApp.Models;

public class EmailRequest
{
    public List<string> To { get; set; } = null!;

    public string Subject { get; set; } = null!;

    public string HtmlBody { get; set; } = null!;

    public string PlainText { get; set; } = null!;
}
