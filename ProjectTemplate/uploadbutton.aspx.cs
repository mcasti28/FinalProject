using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace accountmanager
{
	public partial class uploadbutton : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void UploadSubmit_Click(object sender, EventArgs e)
		{
			if (Uploader.HasFile)
			{
				try
				{
					string filename = Path.GetFileName(Uploader.FileName);
					Uploader.SaveAs(Server.MapPath("~/") + "uploads/" + filename);
					string[] textLines = File.ReadAllLines(Server.MapPath("~/")+"/uploads/" + filename);

                    var client = new RestClient("https://api.meaningcloud.com/summarization-1.0");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("content-type", "application/x-www-form-urlencoded");
                    request.AddParameter("application/x-www-form-urlencoded", "key=YOUR_KEY_VALUE&txt=YOUR_TXT_VALUE&url=YOUR_URL_VALUE&doc=YOUR_DOC_VALUE&sentences=YOUR_SENTENCES_VALUE", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);


                    StatusLabel.Text = "Success! First line of text: "+textLines[0];


				}
				catch (Exception ex)
				{
					StatusLabel.Text = "Error: " + ex.Message;
				}
			}
		}
	}
}