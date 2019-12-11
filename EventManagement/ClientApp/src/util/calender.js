export const getDaysInMonth = async (month) => {
    //addTokenToHeader();
    try {
         console.log("making api call")
        const events = await EventService.getEvents1(pageNumber, keyword)
        return handleResponse(events);
    } catch (error) {
      displayError(error);
    }
  };