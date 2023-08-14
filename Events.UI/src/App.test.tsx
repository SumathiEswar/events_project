import React from 'react';
import { render, screen } from '@testing-library/react';
import App from './App';

test('renders Events Events Text', () => {
  render(<App />);
  const linkElement = screen.getByText(/Events Events/i);
  expect(linkElement).toBeInTheDocument();
});
