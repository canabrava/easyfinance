import { login, register } from './registerService';

describe('register', () => {
  let originalEnv;

  beforeEach(() => {
    // Store original process.env
    originalEnv = { ...process.env };
    // Mock process.env for the test
    process.env.NEXT_PUBLIC_REGISTER_SYSTEM_API_URL = 'http://localhost:3000';

    // Mock the fetch function
    global.fetch = jest.fn().mockResolvedValue({
      ok: true,
      json: jest.fn().mockResolvedValue({}),
    });
  });

  afterEach(() => {
    // Restore original process.env after each test
    process.env = originalEnv;
    jest.resetAllMocks();
  });


  it('should return the response JSON if the request is successful', async () => {
    const responseJson = { success: true };

    // @ts-ignore
    global.fetch.mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(responseJson),
    });

    const result = await register('John Doe', 'john@example.com', 'password');

    expect(result).toEqual(responseJson);
  });

});

describe('login', () => {
    let originalEnv;

  beforeEach(() => {
    // Store original process.env
    originalEnv = { ...process.env };
    // Mock process.env for the test
    process.env.NEXT_PUBLIC_REGISTER_SYSTEM_API_URL = 'http://localhost:3000';

    // Mock the fetch function
    global.fetch = jest.fn().mockResolvedValue({
      ok: true,
      json: jest.fn().mockResolvedValue({}),
    });
  });

  afterEach(() => {
    // Restore original process.env after each test
    process.env = originalEnv;
    jest.resetAllMocks();
  });

  it('should return the response JSON if the request is successful', async () => {
    const responseJson = { success: true };

    // @ts-ignore
    global.fetch.mockResolvedValueOnce({
      ok: true,
      json: jest.fn().mockResolvedValue(responseJson),
    });

    const result = await login('john@example.com', 'password');

    expect(result).toEqual(responseJson);
  });

});