# 1/ Project Description
- This project contains interfaces used to build entities for the Infrastructure layer of other API services.
- It also includes enums, constants used within these entities to enable easy sharing and maintain consistency across API services.

# 2/ Project Structure
- This project is organized into the following directories:
  - `Interfaces`: Contains interfaces that define the structure of entities used in the Infrastructure layer of other projects.
  - `Enums`: Contains enumerations that are used in entities across various API Service Projects.
  - `Constants`: Contains constant values that are shared across different parts of other API Service projects.

# 3/ Project Rules
- This project must not have any dependencies on other projects.
- It should only contain interfaces, enums and constants, without any concrete implementations or business logic.

# 4/ AI Agent Rules
- AI agents must not modify or add any business logic, concrete implementations, or dependencies to this project.