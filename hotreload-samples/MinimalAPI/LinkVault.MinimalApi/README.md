# LinkVault - Minimal API

A URL bookmarking and link management Web API built with ASP.NET Core **Minimal APIs**.

## Purpose

This sample demonstrates Hot Reload capabilities with Minimal API endpoint definitions, route groups, endpoint filters, and service logic.

## Running

```bash
dotnet watch
```

The API will start at `http://localhost:5180`. OpenAPI documentation is available at `/openapi/v1.json` in development mode.

## Hot Reload Test Points

- Modify endpoint route paths in the `Endpoints/` files
- Add new endpoints to existing route groups
- Change response DTO shapes in `Models/DTOs.cs`
- Update service business logic in `Services/`
- Change OpenAPI metadata/descriptions on endpoints
- Add new route groups
- Update middleware pipeline in `Program.cs`

## API Endpoints

### Links
- `GET /api/links` — List links (with `?search=`, `?collectionId=`, `?favoritesOnly=` filters)
- `GET /api/links/{id}` — Get link by ID
- `POST /api/links` — Create link
- `PUT /api/links/{id}` — Update link
- `DELETE /api/links/{id}` — Delete link
- `POST /api/links/{id}/click` — Record a click
- `GET /api/links/favorites` — Get favorite links

### Collections
- `GET /api/collections` — List collections
- `GET /api/collections/{id}` — Get collection with links
- `POST /api/collections` — Create collection
- `PUT /api/collections/{id}` — Update collection
- `DELETE /api/collections/{id}` — Delete collection
- `GET /api/collections/{id}/links` — Get links in collection

### Tags
- `GET /api/tags` — List tags with usage counts
- `POST /api/tags` — Create tag
- `DELETE /api/tags/{id}` — Delete tag

### Stats
- `GET /api/stats` — Overall statistics
- `GET /api/stats/top-clicked` — Most clicked links (`?count=` parameter)
