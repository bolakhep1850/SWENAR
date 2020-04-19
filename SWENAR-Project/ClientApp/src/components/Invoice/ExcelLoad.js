import React, { Component } from "react";

class ExcelLoad extends Component {
  static displayName = ExcelLoad.name;

  constructor(props) {
    super(props);
    this.state = {};
  }
  // component used to Post an excel file to the database
  render() {
    return (
      <div>
        <form
          enctype='multipart/form-data'
          method='post'
          action='https://localhost:44375/api/Invoice/Load'
        >
          <input
            type='file'
            name='excelFile'
            id='excel-file'
            className='form-control'
          />
          <button type='submit'>Submit</button>
        </form>
      </div>
    );
  }
}

export default ExcelLoad;
