# Emailit.Client
This is an unofficial open-source HTTP client designed for use with the [Emailit API](https://docs.emailit.com/).

The client is based on [Flurl](https://github.com/tmenier/Flurl).

## Basic usage

### 1. Initialize the Client

You can simply instantiate `EmailitClient` using the default constructor:
```csharp
var client = new EmailitClient();
```

However, in order to use ``EmailitClient``, you will need your [Emailit API Key](https://docs.emailit.com/authentication) anyway. You can provide it in one of four ways:

- Via the constructor:

```csharp
var client = new EmailitClient("youApiKey");
```

- Via an `EmailitConfiguration` object:

```csharp
var config = new EmailitConfiguration
{
	ApiKey = "yourApiKey",
};

var client = new EmailitClient(config);
```

- Via a configuration action:

```csharp
var client = new EmailitClient(config => {
	config.ApiKey = "yourApiKey"
})
```

- After initialization using the `UseApiKey()` method:

```csharp
var client = new EmailitClient();

client.UseApiKey("yourApiKey");
```

### 2. Creating Emailit Messages

Before sending an email using `EmailitClient`, you need to create an email message. Emailit messages are represented by the `EmailitMessage` model. You can create one in two ways:

- Using an `EmailitMessage` literal:

```csharp
var message = new EmailitMessage
{
	From = some@email.com",
	To = "recipient@email.com",
	Subject = "Some subject",
	Text = "Some text",
}
```

- Using an `EmailitMessageBuilder` 

```csharp
var message = new EmailitMessageBuilder()
	.From("some@email.com")
	.To("recipient@email.com")
	.ReplyTo("some.other@email.com")
	.Subject("Some subject")
	.WithHtmlContent("<h1>Some HTML</h1>")
	.Message;
```

**NOTE:** It is recommended to build `EmailitMessage` instances using `EmailitMessageBuilder`, as it provides some basic null checks and validation. If you use `EmailitMessage` directly, you must handle validation yourself..

### 3. Support for Email Attachments

`EmailitMessage` provides support for email attachments, represented by `EmailitAttachment` object:

- You can initialize it via the constructor:

```csharp
var attachment = new EmailitAttachment(
	"someattachment.pdf",
	"<based-64 array of bytes>"
);
```

- Or via object literal: 

```csharp
var attachment = new EmailitAttachment
{
	FileName: "somefile.pdf",
	Content: "<based-64 array of bytes>"
};
```

- You can also create it directly from a byte array:

```csharp
var attachment = EmailitAttachment.FromByteArray(<array of bytes here>, "filename.png");
```

Once created, attachments can be easily added to an `EmailitMessage`:

```csharp
var attachment = new EmailitAttachment(); 
var message = new EmailitMessage();

message.AddAttachment(attachment);
```

Attachment can be also added as array of bytes, directly to `EmailitMessage`:

```csharp
message.AddAttachment(<array of bytes>, "filename.png");
```

### 4. Sending e-mails

Once your `EmailitMessage` is ready, you can send it using: 

- `EmailitClient`:

```csharp
var client = new EmailitClient("yourApiKey");

var message = new EmailitMessageBuilder()
	.From("some@email.com")
	.To("recipient@email.com")
	.ReplyTo("some.other@email.com")
	.Subject("Some subject")
	.WithTextContent("Some text")
	.Message;

await client.SendEmailAsync(message);
```

- Directly from `EmailitMessage` (using an extension method):

```csharp
var message = new EmailitMessageBuilder()
	.From("some@email.com")
	.To("recipient@email.com")
	.ReplyTo("some.other@email.com")
	.Subject("Some subject")
	.WithTextContent("Some text")
	.Message;

await message.SendAsync("yourApiKey");
```

### 5. Overriding default settings

The default `EmailitClient` settings are defined in the `protected virtual DefaultBaseUrl` property, which:
- Provides default `JsonSerializerOptions` for the `System.Text.Json` serializer
- Verifies that the `ApiKey` is supplied (throws if not)
- Supplies the API Call with the provided `ApiKey`
- Builds a basic `Url` object (a [Flurl object](https://flurl.dev/docs/fluent-url/)) using default `_baseUrl` and `_version`

To override the default behavior, simply create a custom class that derives from 'EmailitClient'. You can then redefine the 'DefaultBaseUrl' behavior as needed.

## What is not covered 

`EmailitClient` covers three crutial Emailit functionalities: 
- Managing Credentials
- Managing Sending Domains
- Sending Emails

It **does not** cover managing audiences, contacts, events and campaigns. Feel free to contribute and help expand this functionality!

## Roadmap 

In the nearest future, I'd like to: 
- Create a separate package that provides `FluentValidation` for Emailit objects
- Implement missing `EmailitClient` methods and models (see above)
- Enhance support for Dependency Injection (currently uses a simple “clientless” pattern from [Flurl](https://flurl.dev/docs/clients/))
- Improve error handling, especially for HTTP status codes 

Feel free to contribute!