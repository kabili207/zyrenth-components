using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebTest
{
	public partial class Default : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			/*ColorSwatch3.SecondaryColors.AddRange(new string [] {"ff00ff", "0f382a" });*/
		}

		protected void ModalPopup1_ButtonClicked(object sender, Zyrenth.Web.ModalPopup.ModalButtonEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Derp":
					break;
				case "Deee":
					break;
			}
		}
	}
}