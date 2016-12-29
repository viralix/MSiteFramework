string allInfo = "";
allInfo += "==== DATA ====" + Environment.NewLine;
allInfo += "Address:" + server.address + Environment.NewLine;
allInfo += "Get:" + server.get + Environment.NewLine;
allInfo += "Post:" + server.post + Environment.NewLine;
allInfo += "Has data:" + server.hasData.ToString() + Environment.NewLine;
allInfo +=  Environment.NewLine;
allInfo += "==== HTTP ====" + Environment.NewLine;
allInfo += "URL:" + server.request.Url + Environment.NewLine;
allInfo += "Method:" + server.request.Method +Environment.NewLine;
allInfo += "Path:" + server.request.Path + Environment.NewLine;

Element[] head = {
    new Element("title", "Information"),
};

Element[] body = {
	new Element("h1", "Request Information"),
    new Element("pre", allInfo),
};