[![](https://img.shields.io/nuget/v/soenneker.granola.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclient/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclient/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.granola.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.granola.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.granola.openapiclient/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.granola.openapiclient/actions/workflows/codeql.yml)

# Soenneker.Granola.OpenApiClient

A Kiota-generated .NET client for Granola notes, transcripts, folders, audit events, and webhook endpoints.

## Install

```bash
dotnet add package Soenneker.Granola.OpenApiClient
```

For dependency injection, authentication configuration, and managed client reuse, install `Soenneker.Granola.OpenApiClientUtil` instead. It exposes this generated client over the long-lived HTTP client registration used by the Granola packages.

## Direct construction

```csharp
using System.Net.Http.Headers;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Granola.OpenApiClient;
using Soenneker.Granola.OpenApiClient.Models;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("https://public-api.granola.ai")
};
httpClient.DefaultRequestHeaders.Authorization =
    new AuthenticationHeaderValue("Bearer", apiKey);

var adapter = new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient);

var client = new GranolaOpenApiClient(adapter);
```

Reuse the `HttpClient`, request adapter, and generated client rather than constructing them per request.

## List notes

```csharp
ListNotesOutput? page = await client.V1.Notes.GetAsync(config =>
{
    config.QueryParameters.PageSize = 50;
    config.QueryParameters.Cursor = cursor;
}, cancellationToken);
```

Individual resources use Kiota indexers. For example, `client.V1.Notes[noteId].GetAsync()` retrieves a note and `client.V1.Notes[noteId].Transcript.GetAsync()` retrieves its transcript.

## API surface

- `client.V1.Notes` lists notes and accesses notes and transcripts by ID.
- `client.V1.Folders` lists folders.
- `client.V1.Audit` retrieves audit events.
- `client.V1.WebhookEndpoints` lists, creates, updates, and deletes webhook endpoints.

Generated request methods return `null` when Kiota receives no response body. HTTP and API failures are surfaced through Kiota exceptions. Cancellation tokens are forwarded to the underlying request.

Files under `src` are generated and may be replaced by the generator. Put application-specific behavior in a separate project or partial class rather than editing generated request builders and models.
