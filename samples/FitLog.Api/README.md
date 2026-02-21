# FitLog - Web API (Controllers)

A fitness workout logging and tracking Web API built with ASP.NET Core **Controllers**.

## Purpose

This sample demonstrates Hot Reload capabilities with controller-based Web API projects, including controller actions, model validation, action filters, and service logic.

## Running

```bash
dotnet watch
```

The API will start at `http://localhost:5190`. OpenAPI documentation is available at `/openapi/v1.json` in development mode.

## Hot Reload Test Points

- Modify controller action logic in `Controllers/`
- Add new controller actions/endpoints
- Change DTO/response model shapes in `Models/DTOs.cs`
- Update model validation attributes
- Change service business logic in `Services/`
- Update route attributes on controllers
- Add new controllers
- Modify middleware configuration in `Program.cs`

## API Endpoints

### Workouts
- `GET /api/workouts` — List workouts (`?fromDate=`, `?toDate=`, `?type=` filters)
- `GET /api/workouts/{id}` — Get workout with exercises
- `POST /api/workouts` — Create workout
- `PUT /api/workouts/{id}` — Update workout
- `DELETE /api/workouts/{id}` — Delete workout

### Exercises (nested under workouts)
- `POST /api/workouts/{workoutId}/exercises` — Add exercise to workout
- `PUT /api/workouts/{workoutId}/exercises/{id}` — Update exercise
- `DELETE /api/workouts/{workoutId}/exercises/{id}` — Remove exercise

### Exercise Definitions
- `GET /api/exercise-definitions` — List exercise library
- `GET /api/exercise-definitions/{id}` — Get exercise details
- `POST /api/exercise-definitions` — Add exercise to library
- `PUT /api/exercise-definitions/{id}` — Update exercise definition

### Personal Records
- `GET /api/personal-records` — List all PRs
- `GET /api/personal-records/exercise/{exerciseDefinitionId}` — PRs for specific exercise
- `POST /api/personal-records` — Record a new PR

### Stats
- `GET /api/stats/weekly-summary` — Current week summary
- `GET /api/stats/monthly-summary` — Current month summary
- `GET /api/stats/muscle-group-breakdown` — Volume by muscle group
- `GET /api/stats/progress/{exerciseDefinitionId}` — Progress over time
