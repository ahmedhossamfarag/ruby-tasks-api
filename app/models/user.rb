class User < ApplicationRecord
  has_secure_password
  validates :username, presence: true, uniqueness: true, length: { in: 3..20 }
  has_many :task_notes
end
