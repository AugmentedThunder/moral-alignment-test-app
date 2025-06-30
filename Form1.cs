using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace MoralAlignmentTest
{
    public partial class Form1 : Form
    {
        private class Question
        {
            public string Text { get; }
            public string[] Answers { get; }
            public (int lawful, int good, Dictionary<string, int> customTraitsDelta)[] Scores;

            public Question(string text, string[] answers,
                (int lawful, int good, Dictionary<string, int> customTraitsDelta)[] scores)
            {
                Text = text;
                Answers = answers;
                Scores = scores;
            }
        }

        private List<Question> questions;
        private int currentQuestion = 0;

        private int lawfulPoints = 0;
        private int goodPoints = 0;
        private Dictionary<string, int> customTraits = new();

        private void linkMoreInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(resultUrl))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = resultUrl,
                    UseShellExecute = true
                });
            }
        }

        private int[] selectedAnswers;
        private string resultUrl; // stores the URL for More Info link

        private readonly string[] customTraitNames = new[] { "Steadfast", "Selective", "Cunning", "Compassionate", "Ruthless" };

        public Form1()
        {
            InitializeComponent();
            InitQuestions();
            selectedAnswers = new int[questions.Count];
            for (int i = 0; i < selectedAnswers.Length; i++)
                selectedAnswers[i] = -1;
            LoadQuestion();
            btnBack.Enabled = false;
            lblResult.Text = "";
        }

        private void InitQuestions()
        {
            questions = new List<Question>
            {
                new Question(
                    "You find a wallet full of money on the street. What do you do?",
                    new[]
                    {
                        "Return it to the owner immediately.",
                        "Keep the money but try to find the owner later.",
                        "Use the money for charity anonymously.",
                        "Keep it because no one saw you.",
                        "Donate it to a cause you support."
                    },
                    new[]
                    {
                        (1, 0, new Dictionary<string, int>{{"Steadfast",1}}),
                        (0, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 1, new Dictionary<string, int>{{"Compassionate",1}}),
                        (-1, -1, new Dictionary<string, int>{{"Ruthless",1}}),
                        (-1, 1, new Dictionary<string, int>{{"Cunning",1}})
                    }
                ),

                new Question(
                    "Is it okay to lie if it saves someone’s feelings?",
                    new[]
                    {
                        "No, honesty is always best.",
                        "Only white lies are acceptable.",
                        "It depends on the situation.",
                        "Yes, feelings matter more than truth.",
                        "Lying is a useful tool when necessary."
                    },
                    new[]
                    {
                        (1, 0, new Dictionary<string, int>{{"Steadfast",1}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (-1, 1, new Dictionary<string, int>{{"Compassionate",1}}),
                        (-1, 0, new Dictionary<string, int>{{"Cunning",1}})
                    }
                ),

                new Question(
                    "How do you view rules?",
                    new[]
                    {
                        "Rules are sacred and must always be followed.",
                        "Rules should be respected but can be bent for good reason.",
                        "Rules are guidelines, not laws.",
                        "Rules are often obstacles to freedom.",
                        "Rules are meant to be broken if it benefits me."
                    },
                    new[]
                    {
                        (2, 0, new Dictionary<string, int>{{"Steadfast",2}}),
                        (1, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (-1, 0, new Dictionary<string, int>{{"Cunning",1}}),
                        (-2, 0, new Dictionary<string, int>{{"Ruthless",2}})
                    }
                ),

                new Question(
                    "You see someone being bullied. What do you do?",
                    new[]
                    {
                        "Stand up to the bully, risking your own safety.",
                        "Report the bully to authorities.",
                        "Try to mediate and calm both parties.",
                        "Avoid getting involved; it's not your problem.",
                        "Help the victim secretly and anonymously."
                    },
                    new[]
                    {
                        (1, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (2, 1, new Dictionary<string, int>{{"Steadfast",1}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (-1, -1, new Dictionary<string, int>{{"Ruthless",1}}),
                        (-1, 2, new Dictionary<string, int>{{"Cunning",1}})
                    }
                ),

                new Question(
                    "How do you handle power?",
                    new[]
                    {
                        "Use it to enforce justice fairly.",
                        "Use it to protect those you care about.",
                        "Use it pragmatically for the greater good.",
                        "Use it to advance your own interests.",
                        "Avoid power whenever possible."
                    },
                    new[]
                    {
                        (2, 1, new Dictionary<string, int>{{"Steadfast",2}}),
                        (1, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (-2, -1, new Dictionary<string, int>{{"Ruthless",2}}),
                        (0, 0, new Dictionary<string, int>())
                    }
                ),

                new Question(
                    "You discover a friend has broken the law. What do you do?",
                    new[]
                    {
                        "Report them to the authorities.",
                        "Confront them and encourage them to make amends.",
                        "Keep it secret but advise them to change.",
                        "Ignore it; loyalty comes first.",
                        "Use this knowledge to your advantage."
                    },
                    new[]
                    {
                        (2, 0, new Dictionary<string, int>{{"Steadfast",1}}),
                        (1, 1, new Dictionary<string, int>{{"Compassionate",1}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (-1, 0, new Dictionary<string, int>{{"Ruthless",1}}),
                        (-1, -1, new Dictionary<string, int>{{"Cunning",1}})
                    }
                ),

                new Question(
                    "What's your approach to helping strangers?",
                    new[]
                    {
                        "Always help when possible.",
                        "Help if it doesn't put you at risk.",
                        "Help only if they help others too.",
                        "Rarely help; people should fend for themselves.",
                        "Help but expect something in return."
                    },
                    new[]
                    {
                        (0, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (0, -2, new Dictionary<string, int>{{"Ruthless",2}}),
                        (0, 0, new Dictionary<string, int>{{"Cunning",1}})
                    }
                ),

                new Question(
                    "In a conflict, do the ends justify the means?",
                    new[]
                    {
                        "No, the means must be just.",
                        "Sometimes, if the outcome is good.",
                        "It depends on who benefits.",
                        "Yes, whatever it takes.",
                        "I find loopholes to win."
                    },
                    new[]
                    {
                        (1, 1, new Dictionary<string, int>{{"Steadfast",1}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (-2, -1, new Dictionary<string, int>{{"Ruthless",2}}),
                        (-1, 0, new Dictionary<string, int>{{"Cunning",2}})
                    }
                ),

                new Question(
                    "How important is personal freedom to you?",
                    new[]
                    {
                        "Very important, above all else.",
                        "Important but balanced with responsibility.",
                        "Somewhat important, but rules matter too.",
                        "Not very important, order is key.",
                        "Freedom only for the strong."
                    },
                    new[]
                    {
                        (-2, 0, new Dictionary<string, int>{{"Cunning",2}}),
                        (0, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (1, 0, new Dictionary<string, int>{{"Steadfast",1}}),
                        (2, 0, new Dictionary<string, int>{{"Steadfast",2}}),
                        (-1, -1, new Dictionary<string, int>{{"Ruthless",1}})
                    }
                ),

                new Question(
                    "You witness corruption in your workplace. What do you do?",
                    new[]
                    {
                        "Report it to higher authorities.",
                        "Confront the offenders privately.",
                        "Ignore it; focus on your own work.",
                        "Use it to your advantage.",
                        "Expose it publicly no matter the risk."
                    },
                    new[]
                    {
                        (2, 1, new Dictionary<string, int>{{"Steadfast",2}}),
                        (1, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (-2, 0, new Dictionary<string, int>{{"Cunning",2}}),
                        (1, 2, new Dictionary<string, int>{{"Compassionate",2}})
                    }
                ),

                new Question(
                    "What motivates you the most?",
                    new[]
                    {
                        "Justice and fairness.",
                        "Protecting loved ones.",
                        "Personal growth.",
                        "Power and control.",
                        "Helping others."
                    },
                    new[]
                    {
                        (2, 2, new Dictionary<string, int>{{"Steadfast",2}}),
                        (1, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (0, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (-2, -1, new Dictionary<string, int>{{"Ruthless",2}}),
                        (0, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                    }
                ),

                new Question(
                    "Do you believe in sacrifice for the greater good?",
                    new[]
                    {
                        "Yes, always if it helps others.",
                        "Sometimes, if necessary.",
                        "Rarely, only if absolutely needed.",
                        "No, individuals should be protected.",
                        "Only if it benefits me or my group."
                    },
                    new[]
                    {
                        (0, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (0, -1, new Dictionary<string, int>{{"Steadfast",1}}),
                        (-1, 0, new Dictionary<string, int>{{"Cunning",1}}),
                    }
                ),

                new Question(
                    "How do you treat those weaker than you?",
                    new[]
                    {
                        "Protect and support them.",
                        "Help if they help themselves.",
                        "Ignore them; survival is personal.",
                        "Use them to advance yourself.",
                        "Respect those who earn it."
                    },
                    new[]
                    {
                        (0, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (0, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (0, -1, new Dictionary<string, int>{{"Ruthless",1}}),
                        (-1, -1, new Dictionary<string, int>{{"Cunning",2}}),
                        (1, 0, new Dictionary<string, int>{{"Steadfast",1}}),
                    }
                ),

                new Question(
                    "What is your attitude toward revenge?",
                    new[]
                    {
                        "Never seek revenge; forgive.",
                        "Revenge only if justice is served.",
                        "Revenge is natural but controlled.",
                        "Always take revenge when wronged.",
                        "Use revenge as a strategic tool."
                    },
                    new[]
                    {
                        (0, 1, new Dictionary<string, int>{{"Compassionate",2}}),
                        (1, 0, new Dictionary<string, int>{{"Steadfast",1}}),
                        (0, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (-1, -1, new Dictionary<string, int>{{"Ruthless",2}}),
                        (-2, 0, new Dictionary<string, int>{{"Cunning",2}}),
                    }
                ),

                new Question(
                    "How do you feel about traditions?",
                    new[]
                    {
                        "Respect and uphold traditions.",
                        "Follow traditions if they make sense.",
                        "Respect some, challenge others.",
                        "Traditions are often outdated and limiting.",
                        "Ignore traditions; only results matter."
                    },
                    new[]
                    {
                        (2, 0, new Dictionary<string, int>{{"Steadfast",2}}),
                        (1, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (-1, 0, new Dictionary<string, int>{{"Cunning",1}}),
                        (-2, 0, new Dictionary<string, int>{{"Ruthless",2}}),
                    }
                ),

                new Question(
                    "Your community needs a leader. What qualities matter most?",
                    new[]
                    {
                        "Integrity and fairness.",
                        "Compassion and understanding.",
                        "Pragmatism and decisiveness.",
                        "Strength and fearlessness.",
                        "Cunning and strategy."
                    },
                    new[]
                    {
                        (2, 2, new Dictionary<string, int>{{"Steadfast",2}}),
                        (0, 2, new Dictionary<string, int>{{"Compassionate",2}}),
                        (1, 1, new Dictionary<string, int>{{"Selective",1}}),
                        (-1, -1, new Dictionary<string, int>{{"Ruthless",2}}),
                        (-1, 0, new Dictionary<string, int>{{"Cunning",2}}),
                    }
                ),

                new Question(
                    "Do you believe rules should apply equally to everyone?",
                    new[]
                    {
                        "Yes, without exception.",
                        "Mostly, but some deserve special treatment.",
                        "Depends on the person and situation.",
                        "No, rules are for others.",
                        "Rules exist to be exploited."
                    },
                    new[]
                    {
                        (2, 0, new Dictionary<string, int>{{"Steadfast",2}}),
                        (1, 0, new Dictionary<string, int>{{"Selective",1}}),
                        (0, 0, new Dictionary<string, int>()),
                        (-1, 0, new Dictionary<string, int>{{"Ruthless",1}}),
                        (-2, 0, new Dictionary<string, int>{{"Cunning",2}}),
                    }
                ),
            };
        }

        private void LoadQuestion()
        {
            var q = questions[currentQuestion];
            lblQuestion.Text = $"Question {currentQuestion + 1}/{questions.Count}:\n{q.Text}";

            radioButton1.Text = q.Answers[0];
            radioButton2.Text = q.Answers[1];
            radioButton3.Text = q.Answers[2];
            radioButton4.Text = q.Answers[3];
            radioButton5.Text = q.Answers[4];

            // Restore previous selection if any
            switch (selectedAnswers[currentQuestion])
            {
                case 0: radioButton1.Checked = true; break;
                case 1: radioButton2.Checked = true; break;
                case 2: radioButton3.Checked = true; break;
                case 3: radioButton4.Checked = true; break;
                case 4: radioButton5.Checked = true; break;
                default:
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    radioButton5.Checked = false;
                    break;
            }

            btnBack.Enabled = currentQuestion > 0;
            btnNext.Text = currentQuestion == questions.Count - 1 ? "Finish" : "Next";
            lblResult.Text = "";
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int answer = GetSelectedAnswer();
            if (answer == -1)
            {
                MessageBox.Show("Please select an answer before proceeding.");
                return;
            }

            selectedAnswers[currentQuestion] = answer;

            if (currentQuestion < questions.Count - 1)
            {
                currentQuestion++;
                LoadQuestion();
            }
            else
            {
                CalculateResult();
            }
        }

        private int GetSelectedAnswer()
        {
            if (radioButton1.Checked) return 0;
            if (radioButton2.Checked) return 1;
            if (radioButton3.Checked) return 2;
            if (radioButton4.Checked) return 3;
            if (radioButton5.Checked) return 4;
            return -1;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentQuestion > 0)
            {
                currentQuestion--;
                LoadQuestion();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            currentQuestion = 0;
            lawfulPoints = 0;
            goodPoints = 0;
            customTraits.Clear();
            selectedAnswers = new int[questions.Count];
            for (int i = 0; i < selectedAnswers.Length; i++)
                selectedAnswers[i] = -1;
            LoadQuestion();
            lblResult.Text = "";
        }

        private void CalculateResult()
        {
            lawfulPoints = 0;
            goodPoints = 0;
            customTraits.Clear();
            foreach (var trait in customTraitNames)
                customTraits[trait] = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                int answer = selectedAnswers[i];
                var score = questions[i].Scores[answer];
                lawfulPoints += score.lawful;
                goodPoints += score.good;

                foreach (var kvp in score.customTraitsDelta)
                {
                    if (customTraits.ContainsKey(kvp.Key))
                        customTraits[kvp.Key] += kvp.Value;
                    else
                        customTraits[kvp.Key] = kvp.Value;
                }
            }

            string alignmentLawChaos = lawfulPoints > 0 ? "Lawful" : lawfulPoints < 0 ? "Chaotic" : "Neutral";
            string alignmentGoodEvil = goodPoints > 0 ? "Good" : goodPoints < 0 ? "Evil" : "Neutral";

            // Find highest custom trait for prefix or suffix
            string topTrait = null;
            int topValue = int.MinValue;
            foreach (var kvp in customTraits)
            {
                if (kvp.Value > topValue)
                {
                    topValue = kvp.Value;
                    topTrait = kvp.Key;
                }
            }

            // Compose final alignment with one trait either at start or end randomly
            // For consistency, put the trait at the start if positive, else at the end
            string result;
            if (topValue > 0)
                result = $"{topTrait} {alignmentLawChaos} {alignmentGoodEvil}";
            else
                result = $"{alignmentLawChaos} {alignmentGoodEvil}";

            lblResult.Text = $"Your alignment is: {result}";
            string wikiTitle = result.Replace(" ", "_");
            resultUrl = $"https://iwiki.net/{wikiTitle}";
            linkMoreInfo.Text = "More Info";
            linkMoreInfo.Visible = true;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Opens the URL in the user's default web browser
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://thunder.me/",
                UseShellExecute = true
            });
        }
    }
}
