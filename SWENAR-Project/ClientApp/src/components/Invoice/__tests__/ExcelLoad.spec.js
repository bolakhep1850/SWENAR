import React from "react";
import ExcelLoad from "../ExcelLoad";
import { mount } from "enzyme";

describe("ExcelLoad tests", () => {
  it("Excel load renders corrrectly", () => {
    const props = {};
    const wrapper = mount(<ExcelLoad {...props}></ExcelLoad>);
    expect(wrapper).toMatchSnapshot();
    wrapper.unmount();
  });
});
