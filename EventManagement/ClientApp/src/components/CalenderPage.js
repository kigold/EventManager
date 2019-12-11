import React, { Component } from 'react';
import EventService, {getEvents} from '../services/eventService'
import { Calender } from './Calender';


export class CalenderPage extends Component {

    constructor(props) {
        super(props);
        this.state = { eventsData: [], loading: true};
    }

    async componentDidMount() {
        //make api call
        const result = await getEvents(1,"");
        this.setState({eventsData:result, loading: false})
    }

    static renderCalender(eventsData) {
        return (
            <div> 
                <Calender eventsData={eventsData} />
            </div>
            );
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : CalenderPage.renderCalender(this.state.eventsData)
        return (
            <div>
                {contents}      
            </div>
        );
    }
}

