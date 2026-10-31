# CartOpt development guidelines

## Architecture

CartOpt is a modular monolith.

Projects:

- CartOpt.Domain
- CartOpt.Application
- CartOpt.Infrastructure
- CartOpt.Api

Dependency direction:

Api -> Application
Api -> Infrastructure
Infrastructure -> Application
Application -> Domain
Infrastructure -> Domain

Domain must not reference Infrastructure or Api.

## General C#

- Target .NET 10.
- Nullable reference types are enabled.
- Warnings are treated as errors.
- Prefer simple code over speculative abstractions.
- Do not introduce new dependencies without justification.
- Do not introduce generic repositories.

## Async

- Use async APIs for I/O.
- Propagate CancellationToken where appropriate.
- Do not use .Result or .Wait().

## Database

- PostgreSQL is the primary database.
- EF Core will be used for persistence.
- Do not design persistence models before domain requirements are defined.

## Tests

- Domain logic should be covered by unit tests.
- Database behavior should be covered by integration tests against real PostgreSQL.
- Bug fixes should include regression tests.

## Language

- Respond to the developer in Russian unless explicitly asked otherwise.
- Explain implemented changes and architectural decisions in Russian.
- Write code comments and XML documentation in Russian when comments are actually useful.
- Do not add comments that merely repeat what the code already says.
- Keep identifiers, type names, method names, API contracts, and other code-facing names in English.
