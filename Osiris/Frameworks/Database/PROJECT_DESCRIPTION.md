# 1/ Project Description
- This is project that contain interfaces, class that help to create repository & configuration for Entities which use in infrastructure layer of other API Service projects.

# 2/ Project Rules
- This project only depend on Domain project in Frameworks folder of this solution.
- This project not contian business logic just contain general logic for entities in Repository & Configuration class.

# 3/ Project Structure
- This project is organized into the following directories:
  - `Core`: Contains generic repository, entity type configuration interfaces and classes that provide data access functionality for entities.
  - `Constants`: Contains some constant values that are used find database connection string in appsetting.json of other API Service projects.

# 4/ AI Agent Rules
- AI agents must not modify or add any business logic, concrete implementations, or dependencies to this project.