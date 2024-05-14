using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BankInstructionApp.Models
{
	public class OperationType
	{
		[Key]
		public int opreationID { get; set; }
		public string BankOperationTpye { get; set; }
		public string Description { get; set; }
		public object operationTypeID { get; internal set; }
		public virtual ICollection<InstructionViewModel> Instructions { get; set; }

	}
}