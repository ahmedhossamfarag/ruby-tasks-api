require 'rails_helper'

RSpec.describe User, type: :model do

  context :username do
    it "test username validation" do
      user = User.new(username: "UserName", password: "password")
      expect(user.save).to eq(true)
      user = User.new(username: "UserName", password: "12345678")
      expect(user.save).to eq(false)
      user = User.create(password: "password")
      expect(user.save).to eq(false)
      user = User.create(username: "ah", password: "password")
      expect(user.save).to eq(false)
    end
  end

  context :password_digest do
    it "test password validation" do
      user = User.create(username: "UserName", password: "password")
      expect(user.save).to eq(true)
      user = User.create(username: "UserName")
      expect(user.save).to eq(false)
    end
  end
end
