class UsersController < ApplicationController
  def create
    user = User.new(params.require(:user).permit(:username, :password))
    if user.save
      render status: :created
    else
      render json: user.errors.full_messages, status: :bad_request
    end
  end

  def login
    user = User.authenticate_by(username: params.require(:user)[:username], password: params.require(:user)[:password])
    if user.nil?
      render json: "User not found", status: :bad_request
    else
      session[:user_id] = user.id
      render status: :ok
    end
  end

  def logout
    if session[:user_id].nil?
      render status: :unauthorized
    else
      session[:user_id] = nil
      render status: :ok
    end
  end
end
