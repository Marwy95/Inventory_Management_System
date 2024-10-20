namespace Inventory_Management_System.VerticalSlice.Common.Services.RabbitMQServices.RabbitMQMessages
{
    public class RabbitMQBaseMessage
    {
        public DateTime SentDate { get; set; }
        public string Sender { get; set; }
        public string Action { get; set; }
        public virtual string Type { get; set; }
    }
}
