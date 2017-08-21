# PassKit C# API Wrapper for PassKit core v2

PassKit enables companies worldwide to create connected online to offline customer experiences.

This repo is an API wrapper class for the PassKit API. Full documentation can be found here:<br/>
<a href="https://dev.passkit.net/v2">https://dev.passkit.net/v2</a>

More Information:<br/>
<a href="https://passkit.com/cherrypie/">Start designing Mobile Wallet campaigns</a><br/>
<a href="https://cherrypie.passkit.net">Get an Cherry Pie / API account</a><br/>
<a href="https://passkit.com">About PassKit</a><br/>

**Create a pass**

```
try {
  PassKit pk = new PassKit("<< yourApiKey >>","<< yourApiSecret >>");

  Pass p = new Pass();
  p.templateName = "<< yourTemplateName >>";
  
  string pid = pk.createPass(p);
  
  Console.WriteLine("https://q.passkit.net/p-" + pid);
} 
catch (Exception e) {
  Console.WriteLine(e.Message);
}
```

**Update a pass**

```
try {
  PassKit pk = new PassKit("<< yourApiKey >>","<< yourApiSecret >>");
  
  Pass p = new Pass();
  p.passbook = new PassPassbook();
  p.passbook.bgColor = "#ffffff";
  
  string pid = pk.updatePass("<< yourPassId >>", p);
  
  Console.WriteLine("https://q.passkit.net/p-" + pid);
}
catch (Exception e) {
  Console.WriteLine(e.Message);
}
```

**Retrieve a pass**

```
try {
  PassKit pk = new PassKit("<< yourApiKey >>","<< yourApiSecret >>");
  
  Pass p = pk.retrievePass("<< yourPassId >>");
}
catch (Exception e) {
  Console.WriteLine(e.Message);
}
```