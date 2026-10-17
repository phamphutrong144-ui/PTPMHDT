# Sơ đồ lớp sơ bộ — EduHub

```mermaid
classDiagram
    class Club {
        +Guid Id
        +string Name
        +string Description
        +DateTime CreatedAt
        +IReadOnlyList~Member~ Members
        +AddMember(Member)
        +MemberCount() int
    }
    class Member {
        +Guid Id
        +string Name
        +string Email
        +string StudentCode
        +DateTime JoinedAt
        +MemberStatus Status
        +IsActive() bool
        +Deactivate()
    }
    class Event {
        +Guid Id
        +Guid ClubId
        +string Title
        +DateTime Date
        +int Capacity
        +bool IsOnline
        +HasCapacity(int) bool
    }
    class Registration {
        +Guid Id
        +Guid MemberId
        +Guid EventId
        +DateTime RegisteredAt
        +string Status
        +Confirm()
        +Cancel()
    }
    class CheckIn {
        +Guid Id
        +Guid RegistrationId
        +DateTime CheckedInAt
        +string Method
        +Summary() string
    }
    Club "*" -- "*" Member : có thành viên
    Club "1" --> "*" Event : tổ chức (Event.ClubId)
    Member "1" --> "*" Registration : đăng ký (Registration.MemberId)
    Event "1" --> "*" Registration : nhận (Registration.EventId)
    Registration "1" --> "0..1" CheckIn : điểm danh (CheckIn.RegistrationId)
```
