FROM mcr.microsoft.com/dotnet/aspnet:5.0

# 创建目录
RUN mkdir /app

COPY publish /app

# 设置工作目录
WORKDIR /app

# 暴露80端口
EXPOSE 80

# 设置环境变量
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "CompanyName.ProjectName.HttpApi.Host.dll"]


