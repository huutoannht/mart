using System;
using System.ComponentModel.DataAnnotations;
using Data.DataContract;
using Share.Helper.Models;

namespace Models.BeUser
{
	[Serializable]
	public class BeUserIndexModel : BaseIndexModel<BeUserModel>
	{
        [Display(Name = "TextSearch", ResourceType = typeof(Share.Messages.BackendMessage))]
	    public string TextSearch { get; set; }
	}
}