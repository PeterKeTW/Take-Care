# Take Care 長照機構
## 專案描述：
因應高齡化社會，開發長照機構服務平台，協助家庭用戶線上預約照護人員，並提供完整的後台功能，包括訂單管理、結帳流程與機構端照服員的排程管理。

## 角色：
組員，負責機構簡介、員工介紹、照服員後台(照服員個人檔案、排程和歷史紀錄)頁面。

## 技術(個人部分)：
HTML, CSS, JavaScript, jQuery, Bootstrap, AJAX, .NET 6, ASP.NET MVC, LINQ, MS SQL Server, GitHub

## 成果(個人部分)：
- 建置圖文並茂的機構簡介頁，讓訪客快速了解 Take Care 長照機構的服務內容。  
  程式碼：https://github.com/PeterKeTW/Take-Care/blob/main/Take_Care/Views/AboutUs/Index.cshtml

- 建置照服員介紹頁，提供篩選功能讓使用者可以查看全部、男性或女性的照服員。  
  程式碼：https://github.com/PeterKeTW/Take-Care/blob/main/Take_Care/Views/AboutUs/Staff.cshtml

- 利用LINQ結合多個資料表，傳遞複雜的服務排程紀錄與歷史紀錄至前端。照服員可以根據日期篩選排程，也可取消單筆排程、顯示加總金額。  
  程式碼：https://github.com/PeterKeTW/Take-Care/blob/main/Take_Care/Controllers/StaffController.cs  
  https://github.com/PeterKeTW/Take-Care/blob/main/Take_Care/Views/Staff/Schedule.csht  
  https://github.com/PeterKeTW/Take-Care/blob/main/Take_Care/Views/Staff/Record.cshtml
