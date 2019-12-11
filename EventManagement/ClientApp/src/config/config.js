const config = {
    apiBaseUrl:
      process.env.NODE_ENV === "production"
        ? "https://.com/api/v1"
        : "http://localhost:64197/api",
    imageBaseUrl:
      process.env.NODE_ENV === "production"
        ? "https://res.cloudinary.com/do8buqscj/image/upload/v1556396547/turing_images"
        : "http://localhost:2000/images",
    pageSize : 10
  };
  
  export default config;
  