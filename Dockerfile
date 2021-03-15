# Import Asp.Net Core 5
FROM mcr.microsoft.com/dotnet/aspnet:5.0

# Copy Files  
COPY . /public

# Set Workdir
WORKDIR /public

# Expose Port 5000
EXPOSE 5000

# Run Application
CMD ./DotBlog.Server
