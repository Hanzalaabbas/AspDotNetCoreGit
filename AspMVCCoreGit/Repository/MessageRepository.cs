using AspMVCCoreGit.Controllers;
using AspMVCCoreGit.Models;
using Microsoft.Extensions.Options;

namespace AspMVCCoreGit.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly NewBookAlertConfig _newBookAlertConfig;
        private  NewBookAlertConfig _newBookAlertConfigMonitor;//Reload configuration in singleton service | IOptionsMonitor |
        public MessageRepository( IOptions<NewBookAlertConfig> newBookAlertConfig, IOptionsMonitor<NewBookAlertConfig> newBookAlertConfigMonitor)
        {
            
            _newBookAlertConfig = newBookAlertConfig.Value;

            _newBookAlertConfigMonitor = newBookAlertConfigMonitor.CurrentValue;//Reload configuration in singleton service | IOptionsMonitor 
            newBookAlertConfigMonitor.OnChange(config =>
            {
                _newBookAlertConfigMonitor = config;
            }); 
        }
        public string GetName()
        {
            return _newBookAlertConfig.BookName;
        }
        public string GetName1()
        {
            return _newBookAlertConfigMonitor.BookName;
        }
    }
}
