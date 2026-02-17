namespace BLL.ViewModels.Message;

public class CreateMessageWithoutContractVM
{
    public string ReceiverEmail { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
}