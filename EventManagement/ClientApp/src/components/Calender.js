import React, { Component } from 'react';
import {Day} from './Day';
import { compareDate } from '../utils/dateTimeLib';


export class Calender extends Component {

    constructor(props) {
        super(props);
        this.state = { eventsData: this.props.eventsData,
                       days: this.props.days,
                       loading: true,
                    };
    }

    componentDidMount() {     
        this.setState({eventsData:this.props.eventsData, loading: false})
    }

    static renderCalender(eventsData, days) {
        return (
            <div className="grid-container">
            
                {days.map(day =>  
                    
                    <Day key={day.getDate()} eventsData={eventsData.filter(x => compareDate(x.startDate, day))} day={day}/>   
                )}
            
            </div>
            );
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Calender.renderCalender(this.state.eventsData, this.state.days)
        return (
            <div>
                {contents}     
            </div>
        );
    }
}

