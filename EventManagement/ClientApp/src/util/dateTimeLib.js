export const formatDate = (date) => {
  try {
      return new Date(date).toDateString();
  } catch (error) {
    return "invalid date"
  }
};