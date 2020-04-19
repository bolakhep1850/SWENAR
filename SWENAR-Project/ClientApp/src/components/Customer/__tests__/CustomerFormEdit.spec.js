import React from "react";
import CustomerFormEdit from "../CustomerFormEdit";
import { shallow } from "enzyme";
import axios from "axios";

describe("CustomerForm tests", () => {
  it("shows customer forms with things rendered", () => {
    const props = {
      Name: "",
      number: "",
    };
    const wrapper = shallow(<CustomerFormEdit {...props}></CustomerFormEdit>);
    expect(wrapper).toMatchSnapshot();
  });

  it("Should call setState on input", () => {
    const mockChange = {
      target: {
        name: "input",
        value: "test 123",
      },
    };

    const expected = {
      Name: "",
      number: "",
      input: "test 123",
      error: undefined,
    };

    const props = {
      Name: "",
      number: "",
    };

    const wrapper = shallow(<CustomerFormEdit {...props} />);

    wrapper.instance().onChange(mockChange);

    expect(wrapper.state()).toEqual(expected);
  });

  it("Should call submitToPUT", () => {
    const props = {
      Name: "",
      number: "",
      handleNameNumberchange: jest.spyOn(axios, "put"),
    };

    const mockChange = {
      target: {
        name: "input",
        value: "test 123",
      },
    };

    const wrapper = shallow(<CustomerFormEdit {...props} />);

    wrapper.instance().onChange(mockChange);
    wrapper.instance().submitToPUT();

    wrapper.find("button").simulate("click");
    expect(props.handleNameNumberchange).toHaveBeenCalled();
    wrapper.unmount();
  });
});
