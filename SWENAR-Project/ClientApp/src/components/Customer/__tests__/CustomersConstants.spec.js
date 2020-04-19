import { URL } from "../CustomerConstants";

describe("CustomerConstants tests", () => {
  it("shows customer forms with things rendered", () => {
    expect(URL).toEqual("https://localhost:44375/api/customer");
  });
});
