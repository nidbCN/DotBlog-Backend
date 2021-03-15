# Import Asp.Net Core 5
FROM mcr.microsoft.com/dotnet/aspnet:5.0

# Copy Files  
COPY . /public

# Set Workdir
WORKDIR /public

# Expose Port 80
EXPOSE 80

# Run Application
CMD ./DotBlog.Server
