export const formatDate = (date) => {
  try {
      return new Date(date).toDateString();
  } catch (error) {
    return "invalid date"
  }
};

export const compareDate = (a, b) => {
  try {
      let dateA = new Date(a);
      console.log("dateA", dateA)
      let dateB = new Date(b);
      console.log("dateB", dateB)

      //clear time
      dateA = new Date(dateA.getFullYear(), dateA.getMonth(), dateA.getDate());
      dateB = new Date(dateB.getFullYear(), dateB.getMonth(), dateB.getDate());
      
      console.log("dateA2", dateA)
      console.log("dateB2", dateB)

      console.log("result", dateA.getTime() === dateB.getTime());
      return dateA.getTime() === dateB.getTime();
  } catch (error) {
    return "invalid date"
  }
};