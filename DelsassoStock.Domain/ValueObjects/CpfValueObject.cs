namespace DelsassoStock.Domain.ValueObjects
{
    public sealed class Cpf
    {
        public string Value { get; private set; }

        public Cpf(string cpf)
        {
            if(!IsValid(cpf))
                throw new ArgumentException("CPF inválido", nameof(cpf));

            Value = Format(cpf);
        }

        private static bool IsValid(string cpf)
        {
            cpf = CleanCpf(cpf);

            if(cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplier1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplier2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf[..9];
            int sum = 0;

            for(int i = 0; i < 9; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

            int remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            string digit = remainder.ToString();
            tempCpf += digit;
            sum = 0;

            for(int i = 0; i < 10; i++)
                sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

            remainder = sum % 11;
            remainder = remainder < 2 ? 0 : 11 - remainder;

            digit += remainder.ToString();

            return cpf.EndsWith(digit);
        }

        private static string CleanCpf(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        private static string Format(string cpf)
        {
            return Convert.ToUInt64(CleanCpf(cpf)).ToString(@"000\.000\.000\-00");
        }
    }
}
