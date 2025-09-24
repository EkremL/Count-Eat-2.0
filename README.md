# Count&Eat 2.0 🥗🔥

> 🚧 This project is currently under active development.  
> Features will be added step by step as shown in the roadmap below.

**Count&Eat 2.0** is a complete revamp of my original **Count&Eat** project.  
The first version was built with **MERN stack** (MongoDB, Express, React, Node.js) as my graduation project.  
It worked, but it was rushed and lacked many best practices (manual CSS, API bugs, no optimization).  

Now, I’m rebuilding it from scratch with **modern technologies**:
- **Backend:** ASP.NET Core 9 Web API + Entity Framework Core + PostgreSQL
- **Frontend:** Next.js 15 (React 19) + TailwindCSS + TypeScript
- **Other tools:** AutoMapper, Swagger, Cloudinary (planned), NextAuth/Clerk (planned)

---

## ✨ Features (Current Progress)
- ✅ Ingredients API with DTOs and Service layer
- ✅ EF Core + PostgreSQL integration
- ✅ Clean architecture with AutoMapper and DI
- ⬜ Recipes API (coming soon)
- ⬜ Authentication (NextAuth/Clerk integration)
- ⬜ Calorie tracker & charts
- ⬜ Blog revamp with XP system (inspired by PixelBlog)

---

## 🚀 Why 2.0?
- The first version was MERN stack and had **API/data bugs** (e.g., breakfast fetching problem).
- CSS was written manually → now migrating to **TailwindCSS**.
- No proper separation of concerns → now using **Service + DTO + AutoMapper**.
- From a simple demo → evolving into a **production-grade, SEO-friendly project**.

---

## 🛠 Tech Stack
- **Backend:** ASP.NET Core 9, EF Core, PostgreSQL
- **Frontend:** Next.js 15, React 19, TailwindCSS
- **Tools:** Swagger, AutoMapper, GitHub Actions (CI/CD planned)

---

## 📅 Roadmap
- Add Recipes module
- Implement Auth & Profile
- Add Calorie Tracker + Dashboard
- Blog with XP + Badges
- Dark Mode & i18n (English support)

---

## ⚡ Installation & Run (Backend)

git clone https://github.com/EkremL/CountEat-2.0.git
cd CountEat-2.0/API
dotnet run

## 🧑‍💻 Author
👤 Ekrem Can Lale  
🔗 [GitHub](https://github.com/EkremL) | [LinkedIn](#)
