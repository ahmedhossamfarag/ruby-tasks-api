class TaskNotesController < ApplicationController
  def show
    if session[:user_id].nil?
      render status: :unauthorized
    else
      task_notes = TaskNote.where(user_id: session[:user_id])
      render json: task_notes, status: :ok
    end
  end

  def create
    if session[:user_id].nil?
      render status: :unauthorized
    else
      task_note = TaskNote.new(params.require(:task_note).permit(:title, :description, :priority, :duedate).merge(user_id: session[:user_id]))
      if task_note.save
        render json: task_note, status: :ok
      else
        render json: task_note.errors.full_messages, status: :bad_request
      end
    end
  end

  def update
    if session[:user_id].nil?
      render status: :unauthorized
    else
      task_note = TaskNote.find_by(id: params[:id], user_id: session[:user_id])
      if task_note.nil?
        render status: :not_found
      else
        if task_note.update(params.require(:task_note).permit(:title, :description, :priority, :duedate).merge(user_id: session[:user_id]))
          render json: task_note, status: :ok
        else
          render json: task_note.errors.full_messages, status: :bad_request
        end
      end
    end
  end

  def delete
    if session[:user_id].nil?
      render status: :unauthorized
    else
      task_note = TaskNote.find_by(id: params[:id], user_id: session[:user_id])
      if task_note.nil?
        render status: :not_found
      else
        if task_note.destroy
          render status: :ok
        else
          render json: task_note.errors.full_messages, status: :bad_request
        end
      end
    end
  end
end
