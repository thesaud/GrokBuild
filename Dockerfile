# مرحلة البناء
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# نسخ ملفات المشروع واستعادة الحزم
COPY *.csproj ./
RUN dotnet restore

# نسخ باقي الملفات وبناء المشروع
COPY . ./
RUN dotnet publish -c Release -o out

# مرحلة التشغيل
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out ./

# تعريض المنفذ
EXPOSE 80

# تشغيل التطبيق
ENTRYPOINT ["dotnet", "Grok30.dll"]
