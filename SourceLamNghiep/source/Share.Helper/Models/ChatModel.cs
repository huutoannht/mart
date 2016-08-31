using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;

namespace Share.Helper.Models
{
    [Serializable]
    public class ChatModel
    {
        public bool IsCommand { get; set; }

        public ChatCommandType CommandType { get; set; }

        public string Content { get; set; }

        public string Name { get; set; }

        public List<ChatUserModel> UserGroup { get; set; }

        public bool IsUserSendContent { get; set; }

        public string SessionId { get; set; }

        public string ReceiverSessionId { get; set; }

        public int OrderNumber { get; set; }

        public string ChatTime { get; set; }
    }
}
