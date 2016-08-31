using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Helper.Models
{
    [Serializable]
    public class ChatUserModel
    {
        public ChatUserModel()
        {
            Contents = new List<ChatModel>();
        }

        public string SessionId { get; set; }
        public string Name { get; set; }

        public List<ChatModel> Contents { get; set; }
    }
}
