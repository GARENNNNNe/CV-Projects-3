import json
import tkinter as tk
from tkinter import messagebox

# User class to store user information and preferences
class User:
    def __init__(self, name):
        self.name = name
        self.preferences = {}  # Initialize preferences as an empty dictionary

    def update_preference(self, category, value):
        if not self.preferences:
            self.preferences = {}  # Initialize preferences if it's not already initialized
        self.preferences[category] = value  # Update user preferences

    def get_preference(self, category):
        return self.preferences.get(category, None)  # Get user preference for a given category

    def to_json(self):
        return json.dumps({'name': self.name, 'preferences': self.preferences})  # Convert user data to JSON format

    @staticmethod
    def from_json(json_data):
        data = json.loads(json_data)
        user = User(data['name'])
        user.preferences = data['preferences']
        return user  # Load user data from JSON format


# DietAssistant class to manage user interaction and dietary recommendations
class DietAssistant:
    def __init__(self):
        self.user = User("")  # Initialize with an empty user
        self.previous_responses = {}  # Dictionary to store previous responses

    # Process user response based on the previous question asked
    def process_response(self, response, previous_question):
        response = response.lower()

        if previous_question == 'fruit':
            if 'apple' in response or 'orange' in response or 'banana' in response:
                self.user.update_preference('fruit', True)
                return "That's great! Congratulations on eating fruits!"
            else:
                self.user.update_preference('fruit', False)
                return "It's beneficial to include fruits in your diet."

        if previous_question == 'protein':
            if 'protein' in response or 'chicken' in response or 'fish' in response:
                self.user.update_preference('protein', True)
                return "That's great! Congratulations on including protein-rich foods in your diet."
            else:
                self.user.update_preference('protein', False)
                return "Including a sufficient amount of protein in your diet is important for overall health."

        if previous_question == 'protein_grams':
            try:
                grams = int(response)
                self.user.update_preference('protein_grams', grams)
                if grams >= 20:
                    return "That's good! You're already consuming a sufficient amount of protein."
                else:
                    return "Consider increasing your protein intake to around 20 grams per day for better health benefits."
            except ValueError:
                return "I'm sorry, I didn't understand. Please provide the number of grams of protein you consume per day."

        if previous_question == 'diet_type':
            if 'vegan' in response or 'vegetarian' in response:
                self.user.update_preference('diet_type', response)
                return "It's important to ensure you're getting enough protein from plant-based sources."

        return "Any other questions? Let me know."

    # Run the conversation with the user
    def run_conversation(self):
        print("Good morning.")

        self.user = self.load_user_data() or User(input("Please enter your name: "))

        questions = ['fruit', 'protein', 'protein_grams', 'diet_type']
        current_question = 0

        while current_question < len(questions):
            question = questions[current_question]
            response = input(self.get_question_text(question) + " ")

            system_reply = self.process_response(response, question)
            print(system_reply)

            current_question += 1

        total_protein = self.calculate_total_protein()
        print(f"Based on your responses, your total protein intake is {total_protein} grams per day.")

        if total_protein < 20:
            print("Consider increasing your protein intake to around 20 grams per day for better health benefits.")

        self.save_user_data()
        print("Exiting. Have a great day!")

    # Get the text for the specified question
    def get_question_text(self, question):
        if question == 'fruit':
            return "Have you eaten any fruit today?"
        elif question == 'protein':
            return "Do you eat a lot of protein?"
        elif question == 'protein_grams':
            return "How many grams of protein do you consume per day?"
        elif question == 'diet_type':
            return "What type of diet do you follow (e.g., vegan, vegetarian)?"
        else:
            return "Any other questions? Let me know."

    # Calculate the total protein intake based on user preferences
    def calculate_total_protein(self):
        protein_grams = self.user.get_preference('protein_grams')
        return protein_grams if protein_grams else 0

    # Save user data to a JSON file
    def save_user_data(self):
        with open('user_data.json', 'w') as f:
            f.write(self.user.to_json())

    # Load user data from a JSON file
    def load_user_data(self):
        try:
            with open('user_data.json', 'r') as f:
                return User.from_json(f.read())
        except FileNotFoundError:
            return None


# GUI class to provide a graphical interface for the Diet Assistant
class GUI(tk.Tk):
    def __init__(self):
        super().__init__()
        self.assistant = DietAssistant()
        self.title("Diet Assistant")
        self.geometry("400x300")
        self.current_question = 0
        self.questions = ['fruit', 'protein', 'protein_grams', 'diet_type']
        self.create_widgets()

    # Create GUI widgets
    def create_widgets(self):
        self.question_label = tk.Label(self, text=self.assistant.get_question_text(self.questions[self.current_question]))
        self.question_label.pack()

        self.response_entry = tk.Entry(self)
        self.response_entry.pack()

        self.submit_button = tk.Button(self, text="Submit", command=self.submit_response)
        self.submit_button.pack()

    # Submit user response
    def submit_response(self):
        response = self.response_entry.get()
        system_reply = self.assistant.process_response(response, self.questions[self.current_question])
        messagebox.showinfo("System Reply", system_reply)

        self.current_question += 1
        if self.current_question < len(self.questions):
            self.question_label.config(text=self.assistant.get_question_text(self.questions[self.current_question]))
            self.response_entry.delete(0, tk.END)
        else:
            total_protein = self.assistant.calculate_total_protein()
            messagebox.showinfo("Total Protein Intake", f"Based on your responses, your total protein intake is {total_protein} grams per day.")
            if total_protein < 20:
                messagebox.showinfo("Recommendation", "Consider increasing your protein intake to around 20 grams per day for better health benefits.")
            self.assistant.save_user_data()
            self.destroy()

# Main function to run the GUI version of the Diet Assistant
if __name__ == '__main__':
    app = GUI()
    app.mainloop()
