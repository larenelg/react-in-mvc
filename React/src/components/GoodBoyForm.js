import React, { Component } from 'react';
import goodBoyPicture from './good-boy.jpg';

class GoodBoyForm extends Component {
    constructor(props) {
        super(props);

        this.state = {
            goodBoy: false
        }

        this.handleClick = this.handleClick.bind(this);
    }

    handleClick() {
        if (!this.state.goodBoy)
        {
            this.setState({ goodBoy: true })
        }
    }

    render() {
        if (this.state.goodBoy)
        {
            return <img src={goodBoyPicture}></img>
        }
        return <button onClick={this.handleClick}>Click for good boy</button>;
    }
}

export default GoodBoyForm;