Element[] head = {
	new Element("title", "Results"),
};
string content, subc, id = "";
try
{
foreach(string s in server.post.Split('&'))
{
	string[] x = s.Split('=');
	if(x[0] == "id")
	{
		id = x[1];
		break;
	}
}
if (bool.Parse(id))
{
	content = "Verification passed.";
	subc = "Enjoy this licensed software.";
} else {
	content = "Verification failed.";
	subc = "Please check the domain.";
}
} catch (Exception)
{
	content = "Verification failed.";
	subc = "Please check the domain.";

}
Element[] body = {
	new Element("h1", content),
	new Element("h4", subc),
};