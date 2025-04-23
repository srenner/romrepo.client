import { SystemSetting } from './system-setting';

describe('SystemSetting', () => {
  it('should create an instance', () => {
    expect(new SystemSetting(
      'TestSetting',
      'TestDescr',
      'TestValue',
      'string',
      true,
      false
    )).toBeTruthy();
  });
});
