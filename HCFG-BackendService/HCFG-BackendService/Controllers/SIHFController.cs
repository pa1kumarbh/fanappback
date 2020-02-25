using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HCFGBackendService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SIHFController : Controller
    {
        [HttpGet]
        public String Index()
        {
            return "hello";
        }
		public async Task<string> Get(bool triggerExample = false)
		{
			if (!triggerExample) return "no action done";

			var http = new HttpClient();
			var req = new StringContent(XmlReq);
			var res = await http.PostAsync("http://reporter.sehv.ch/interface/requestdata.aspx", req);
			var xmlRes = await res.Content.ReadAsStringAsync();
			var dir = "Data_NextGames";
			if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

			var path = Path.Combine(dir, $"sihf_ch_gamedata_{DateTime.Now:yyyyMMdd-HHmmss}.xml");
			var fullPath = Path.GetFullPath(path);
			System.IO.File.WriteAllText(fullPath, xmlRes);
			return xmlRes;
		}
		#region Constants
		const string XmlRes = @"<?xml version='1.0' standalone='yes' ?>
	<ReporterMessageResponse ContentType='Empty'
	xmlns='http://reporter.sehv.ch/ReporterMessageResponse.xsd'
	xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
	xsi:schemaLocation='http://reporter.sehv.ch/ReporterMessageResponse.xsd ReporterMessageResponse.xsd'>
		<Status>Successful</Status>
		<StatusText>Successful</StatusText>
	<Content>%content%</Content>
	</ReporterMessageResponse>";
		const string XmlReq = @"<?xml version='1.0' standalone='yes' ?>
	<ReporterMessage ContentType='RequestGames' xmlns='http://reporter.sehv.ch/ReporterMessage.xsd'
	xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'>
	<Content>
		<GamesRequest xmlns='http://reporter.sehv.ch/GamesRequest.xsd'>
			<SearchMode>NextGames</SearchMode>
			<LeagueID>1</LeagueID>
			<RegionID>1</RegionID>
			<GroupID>0</GroupID>
			<QualificationID>1</QualificationID>
			<TournamentID>1</TournamentID>
			<NofGameDays>5</NofGameDays>
		</GamesRequest>
	</Content>
</ReporterMessage>";

		const string GraphQlReq = @"
";
		#endregion
	}
}