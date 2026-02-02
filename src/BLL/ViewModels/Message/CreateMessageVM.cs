namespace BLL.ViewModels.Message;

public class CreateMessageVM
{
    public Guid? ContractId { get; set; }

    public string ReceiverEmail { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;
    
}