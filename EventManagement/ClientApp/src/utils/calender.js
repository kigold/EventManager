export const getDaysInMonth = (month, year) => {
    try{
      const today = new Date();
        month = month === undefined || month === null ? today.getMonth() : month;
        year = year === undefined || year === null ? today.getFullYear() : year;
      console.log(month, year);
      let date = new Date(Date.UTC(year, month, 1));
      let days = [];
      while(date.getMonth() === month){
        days.push(new Date(date));
        date.setDate(date.getDate() + 1);
      }
      return days;
    }catch (error) {
      console.error(error)
    }
  };