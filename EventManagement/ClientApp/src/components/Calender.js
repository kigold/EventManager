import React, { Component } from 'react';
import {Day} from './Day';


export class Calender extends Component {

    constructor(props) {
        super(props);
        this.state = { eventsData: this.props.eventsData, loading: true};
    }

    componentDidMount() {        
        console.log("inside Calender")
        console.log(this.props)
        this.setState({eventsData:this.props.eventsData, loading: false})
    }

    static renderCalender(eventsData) {
        return (
            <div>
                <Day eventsData={eventsData}/>
            </div>
            );
    }

    render() {
        let contents = this.state.loading
        ? <p><em>Loading...</em></p>
        : Calender.renderCalender(this.state.eventsData)
        return (
            <div>
                {contents}     
            </div>
        );
    }
}

